import {Alpine as AlpineType, AlpineComponent} from 'alpinejs';

declare global {
  interface Window {
    showMyModal(): void;
    showSpinner(): void;
    Alpine: AlpineType;
    hideSpinner(): void;
    token: string;
  }
}

export interface LookupDictionary {
  id: string;
  text: string;
}

export interface Pagination {
  offset?: number;
  limit?: number;
  total?: number;
  totalPages?: number;
}


