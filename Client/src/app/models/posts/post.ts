import { Media } from "../media"
import { User } from "../users/user"

export interface Post {
    id: number
    content: string
    postedOn: string
    owner: User
    medias: Media[]
}
