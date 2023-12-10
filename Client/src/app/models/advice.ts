import { Author } from "./author"

export interface Advice {
    id: number
    title: string
    content: string
    imgUrl: string
    publishedOn: string
    author: Author
}
