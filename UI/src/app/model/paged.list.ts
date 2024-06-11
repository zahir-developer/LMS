import { PaginationConfig } from 'ngx-bootstrap/pagination';
export interface PagedListResult<T> {
  pageListConfig: PageListConfig;
  searchText: string;
  items: T[];
}

export interface PageListConfig
{
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}
