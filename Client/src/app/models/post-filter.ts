export interface PostFilter {
    ownerId: number | null
    pageNumber: number
    pageSize: number
    before: string | null
}
