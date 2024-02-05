import { User } from "../users/user"

export interface Review {
    stars: number
    content: string
    reviewedOn: string
    owner: User
}
