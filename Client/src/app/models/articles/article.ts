import { Author } from "../author"

export interface Article {
    id: number
    title: string
    publishedOn: string
    content: string
    imageUrl: string
    author: Author
}
