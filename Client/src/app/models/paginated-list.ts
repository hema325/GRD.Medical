export interface PaginatedList<TData> {
    data: TData[]
    totalPages: number
    pageNumber: number
    pageSize: number
    hasNextPage: boolean
    hasPreviousPage: boolean
}
