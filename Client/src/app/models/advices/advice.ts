import { Author } from "../author"

export interface Advice {
    id: number
    title: string
    content: string
    imageUrl: string
    publishedOn: string
    author: Author
}
