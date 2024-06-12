import { PaginationConfig } from 'ngx-bootstrap/pagination';
export interface PagedListResult<T> {
  pageListConfig: PageListConfig;
  searchText: string;
  items: T[];
}

export interface PageListConfig
{
  pageNumber: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  sortBy: string;
  sortDir: string;
}
