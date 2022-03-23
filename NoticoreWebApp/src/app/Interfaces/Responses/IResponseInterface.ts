export interface IBaseResponseInterface<Type>{
    statusCode: number;
    message: string;
    data: Type;
}