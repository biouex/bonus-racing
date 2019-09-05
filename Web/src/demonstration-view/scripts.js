$(document).ready(function() {

    var ratingListEl = $('#ratingList'),
        playersListEl = $('#playersList'),
        ratingContentEl = $('#ratingContent'),
        playersContentEl = $('#playersContent'),
        timerEl = $('#timer'),
        winnerBlock = $('#winner'),
        winnerId = $('#winner-id');

    var intervalGetRoundDelay = 3000,
        intervalGetRatingDelay = 10000,
        intervalIdGetRound = null,
        intervalIdGetRating = null,
        token = '';

    (function() {
        token = window.location.search.replace('?token=', '');
    })();

    // loop get round
    (function() {
        intervalIdGetRound = setInterval(function() {
            getRound().then((result) => {
                checkStateRound(result);
            });
        }, intervalGetRoundDelay);
    })();

    function finishGetRoundLoop() {
        if (intervalIdGetRound != null) {
            clearInterval(intervalIdGetRound);
            intervalIdGetRound = null;
        }
    }

    // loop watch
    setInterval(watchTick, 1000);

    var currentRound = null,
        currentRating = null,
        inPause = false,
        watchValue = -1;

    // управление состоянием

    function checkStateRound(round) {
        if ((currentRound != null) &&
            (round != null))
            {
                if (currentRound.id == round.id) {
                    setWatchValue(round);
                    
                    if (currentRound.start != round.start) {
                        inPause = false;
                        startRatingUpdateLoop();
                        playersListEl.hide();
                        ratingListEl.show();
                        setWatchValue(round);
                        drawWatch();
                        currentRound = round;
                    }
                    if (currentRound.end != round.end) {
                        if (isEnded(round)) {
                            currentRound = round;
                            completeRound();
                        }
                    }

                    if (currentRound.pause != round.pause) {
                        if (currentRound.pause == null &&
                            round.pause != null) {
                                setWatchValue(round);
                                drawWatch();
                                pause(round);
                            }
                        else if (currentRound.pause != null &&
                            round.pause == null) {
                                setWatchValue(round);
                                drawWatch();
                                takeOf(round);
                            }
                    }
                }
                else {
                    replaceRound(round);
                }
            }
        else if (currentRound == null && round != null) {
            setNewRound(round);
        }
        else if (currentRound != null && round == null) {
            completeRound();
        }
    }

    function setNewRound(round) {
        currentRound = round;
        if (round.start == null) {
            ratingListEl.show();
            inPause = true;
            watchValue = round.duration * 60;
            drawWatch();
            getRating()
                .then(function(rating) {
                    updatePlayersRating(rating);
                });
        } else {
            if (isEnded(round)) {
                completeRound();
            } else {
                startRatingUpdateLoop();
                ratingListEl.show();
                setWatchValue(round);
                drawWatch();
                if (round.pause != null) {
                    pause(round);
                }
            }

        }
    }

    function isEnded(round) {
        return (new Date(round.end) < new Date());
    }

    function replaceRound(round) {
        currentRound = null;
        inPause = false;
        watchValue = -1;
        ratingContentEl.empty();
        playersContentEl.empty();
        playersListEl.hide();
        ratingListEl.hide();
        timerEl.show();
        timerEl.empty();
        winnerBlock.hide();
        setNewRound(round);        
    }

    function completeRound() {
        getRating().then(res => {
            if ((currentRating) && (currentRating.length > 0)) {
                var playerId = currentRating[0].playerId;
                showWinner(playerId);
            }
        });
        finishGetRoundLoop();
        finishRatingUpdateLoop();
    }

    function pause(round) {
        inPause = true;
        timerEl.addClass('timer--pause');
        currentRound.pause = round.pause;
        currentRound.end = round.end;
    }

    function takeOf(round) {
        inPause = false;
        timerEl.removeClass('timer--pause');
        currentRound.pause = null;
        currentRound.end = round.end;
    }

    // цикл секундомера

    function setWatchValue(round) {
        if (round.start == null) {
            watchValue = -1;
            return;
        }
        var newWatchValue;
        if (round.pause != null) {
            var end = new Date(round.end).valueOf(),
                start = new Date(round.start).valueOf(),
                pause = new Date(round.pause).valueOf();
            newWatchValue = Math.floor(((end - start) - (pause - start))/1000);
        } else {
            var end = new Date(round.end).valueOf(),
                now = new Date().valueOf();
            newWatchValue = Math.floor((end - now) / 1000);
        }
        watchValue = newWatchValue;
        if (watchValue < 0) {
            watchValue = -1;
        }
    }

    function watchTick() {
        if (inPause) {
            return;
        }

        if (currentRound != null) {
            setWatchValue(currentRound);
        }

        if (watchValue > 0) {
            drawWatch();
            watchValue--;
        } else if ((watchValue <= 0) && currentRound != null) {
            completeRound();
            watchValue--;
            currentRound = null;
        }
    }

    function drawWatch() {
        var minutes = Math.floor(watchValue / 60);
        var seconds = (watchValue - (minutes * 60));
        var minutesText = (minutes > 9) ? minutes : "0" + minutes;
        var secondsText = (seconds > 9) ? seconds : "0" + seconds;
        timerEl.text(minutesText + ':' + secondsText);
    }

    // поздравление победителя

    function showWinner(playerId) {
        timerEl.hide();
        playersListEl.hide();
        ratingListEl.hide();

        winnerBlock.show();
        winnerId.text(playerId);
    }

    //  цикл получения рейтинга

    function startRatingUpdateLoop() {
        loopFunc();
        intervalIdGetRating = setInterval(loopFunc, intervalGetRatingDelay);
        function loopFunc() {
            getRating()
                .then(function(rating) {
                    updatePlayersRating(rating);
                });
        }
    }

    function finishRatingUpdateLoop() {
        if (intervalIdGetRating != null) {
            clearInterval(intervalIdGetRating);
            intervalIdGetRating = null;
        }
    }

    // обновление списков

    function updatePlayerList(playerList) {
        playersContentEl.empty();
        if (playerList) {
            for (var i = 0, len = playerList.length; i < len; i++) {
                var item = playerList[i];
                playersContentEl.append('<li class="list__item"><div class="list__col">' + item.playerId + '</div></li>');
            }
        }
    }

    function updatePlayersRating(ratingList) {
        ratingContentEl.empty();
        if (ratingList) {
            var count = (ratingList.length > 5) ? 5 : ratingList.length;
            for (var i = 0; i < count; i++) {
                var item = ratingList[i];
                ratingContentEl.append('<li class="list__item"><div class="list__col-1">' + (i+1) + '.' + item.playerId + '</div><div class="list__col-2">' + item.points + '</div></li>');
            }
        }
    }

    //  взаимодействие с сервером

    function getRound() {
        return new Promise(function(resolve, reject) {
            $.ajax({
                method: 'get',
                url: '/api/demoview/',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }).then(function(result) {
                resolve(result);
            });
        });
    }

    function getRating() {
        return new Promise(function(resolve, reject) {
            $.ajax({
                method: 'get',
                url: '/api/demoview/rating',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }).then(function(result) {
                currentRating = result;
                resolve(result);
            });
        });
    }
});