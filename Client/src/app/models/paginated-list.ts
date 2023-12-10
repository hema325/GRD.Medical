export interface PaginatedList<TData> {
    data: TData[]
    totalPages: number
    totalCount: number
    pageNumber: number
    pageSize: number
    hasNextPage: boolean
    hasPreviousPage: boolean
}
