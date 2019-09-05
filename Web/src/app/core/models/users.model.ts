export interface User {
    id: string;
    userName: string;
    roles: string[];
    firstName: string;
    lastName: string;
}

export interface UserCreateModel {
    userName: string;
    password: string;
    roles: string[];
    firstName: string;
    lastName: string;
}

export interface ChangePasswordModel {
    id: string;
    password: string;
}

export interface RoleCheckboxModel {
    id: string;
    label: string;
    checked: boolean;
}