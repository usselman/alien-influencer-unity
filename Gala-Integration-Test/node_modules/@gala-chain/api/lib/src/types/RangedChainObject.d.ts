import { ValidationError } from "class-validator";
import "reflect-metadata";
import { ValidationFailedError } from "../utils";
import { ClassConstructor, Inferred } from "./dtos";
export declare class InvalidRangedKeyError extends ValidationFailedError {
    constructor(message: string);
}
export declare abstract class RangedChainObject {
    serialize(): string;
    validate(): Promise<ValidationError[]>;
    validateOrReject(): Promise<void>;
    toPlainObject(): Record<string, unknown>;
    static deserialize<T>(constructor: ClassConstructor<Inferred<T, RangedChainObject>>, object: string | Record<string, unknown> | Record<string, unknown>[]): T;
    getRangedKey(): string;
    static getRangedKeyFromParts(indexKey: string, parts: unknown[]): string;
    static getStringKeyFromParts(parts: string[]): string;
}
