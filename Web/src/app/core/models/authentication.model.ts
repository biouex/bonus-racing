export interface AuthorizeModel {
    login: string;
    password: string;
}

export interface TokenResult {
    token: string;
}