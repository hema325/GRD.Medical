import { FilterBase } from "../filter-base"

export interface CommentFilter extends FilterBase {
    replyTo: number | null
    postId: number
    before: string | null
}
