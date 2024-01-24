import { User } from "../users/user"
import { Media } from "../media"

export interface Comment {
    id: number
    replyTo: number | null
    content: string
    commentedOn: string
    media: Media
    owner: User
    replies: Comment[]
    totalRepliesCount: number
    postId: number
}
