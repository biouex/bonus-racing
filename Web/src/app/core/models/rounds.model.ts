import { Player } from "./players.model";
import { GameData } from "./gameData.model";

export interface GameRound {
    id: string;
    name: string;
    players: Player[];
    created: string;
    start?: string;
    end?: string;
    state: string;
    pause?: string;
    duration: number;
}

export interface RoundCreateModel {
    name: string;
    duration: number;
    copyBy?: string;
}

export interface RoundUpdateModel {
    id: string;
    name: string;
    duration: number;
}

export interface RoundChangePlayerModel {
    id: string;
    player: Player;
}

export interface RatingItem {
    playerId: string;
    points: number;
}
export interface ArchivalRoundModel {
    id: string;
    name: string;
    players: Player[];
    created: string;
    start: string;
    end: string;
    duration: number;
    gameData: GameData[];
}

export interface ArchivalRoundListItemModel {
    id: string;
    name: string;
    end: string;
}