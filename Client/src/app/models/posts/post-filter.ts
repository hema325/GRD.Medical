import { FilterBase } from "../filter-base"

export interface PostFilter extends FilterBase {
    ownerId: number | null
    before: string | null
}
