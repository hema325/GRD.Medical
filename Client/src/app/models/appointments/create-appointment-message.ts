export interface CreateAppointmentMessage {
    content: string
    images: File[] | null
    appointmentId: number
}
