export interface CreateComment {
    content: string | null
    replyTo: number | null
    postId: number
    image: File | null
}
