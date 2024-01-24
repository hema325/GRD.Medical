import { FilterBase } from "../filter-base"

export interface DoctorFilter extends FilterBase {
    name: string | null | undefined
    experience: number | null | undefined
    specialityId: number | null | undefined
    orderBy: string | null | undefined
}
