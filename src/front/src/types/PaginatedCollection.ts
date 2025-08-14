export interface PaginatedCollection<T> {
    data: T[];
    totalItems: number;
    totalPages: number;
    isLastPage: boolean;
  }