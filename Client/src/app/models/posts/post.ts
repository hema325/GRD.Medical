import { Media } from "../media"
import { User } from "../account/user"

export interface Post {
    id: number
    content: string
    postedOn: string
    owner: User
    medias: Media[]
}
