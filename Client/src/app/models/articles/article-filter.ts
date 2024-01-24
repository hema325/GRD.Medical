import { FilterBase } from "../filter-base"

export interface ArticleFilter extends FilterBase {
    title: string | null
}
