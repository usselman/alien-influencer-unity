import { SchemaObject } from "openapi3-ts";
type ClassConstructor = {
    new (...args: unknown[]): unknown;
};
export type Primitive = "number" | "string" | "boolean" | "null" | "object";
export declare function generateSchema(classType: ClassConstructor): SchemaObject;
export declare function generateResponseSchema(type: ClassConstructor | Primitive | undefined, isArray?: "array"): SchemaObject;
export {};
