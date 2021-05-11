import { ILanguageInterface } from "./ILanguageInterface";

export interface ISourceInterface{
    sourceId: number,
    url: string;
    name: string;
    language: ILanguageInterface;
}