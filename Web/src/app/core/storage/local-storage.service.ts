import { Injectable } from '@angular/core';


@Injectable()
export class LocalStorageService {
  read<T>(key: string): T | null {
    const savedItem = window.localStorage.getItem(key);

    return savedItem ? JSON.parse(savedItem) : null;
  }

  save<T>(key: string, value: T): void {
    window.localStorage.setItem(key, JSON.stringify(value));
  }

  delete<T>(key: string): void {
    window.localStorage.removeItem(key);
  }
}
