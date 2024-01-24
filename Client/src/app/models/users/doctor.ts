import { Language } from "../language"
import { Speciality } from "../specialists/Speciality"

export interface Doctor {
    biography: string
    consultFee: number
    experience: number
    speciality: Speciality
    languages: Language[]
}
