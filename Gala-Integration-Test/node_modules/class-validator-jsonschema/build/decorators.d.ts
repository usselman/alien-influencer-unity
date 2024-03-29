import type { ReferenceObject, SchemaObject } from 'openapi3-ts';
import 'reflect-metadata';
import { IOptions } from './options';
export type DecoratorSchema = ReferenceObject | SchemaObject | ((source: SchemaObject, options: IOptions) => ReferenceObject | SchemaObject);
export declare function JSONSchema(schema: DecoratorSchema): (target: object | Function, key?: string) => void;
export declare function getMetadataSchema(target: object | Function, key: string): DecoratorSchema;
