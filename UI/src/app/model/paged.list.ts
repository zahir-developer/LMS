export interface PagedListResult<T> {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
  items: T[];
}
