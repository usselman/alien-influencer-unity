import type { ValidationMetadata } from 'class-validator/types/metadata/ValidationMetadata';
import type { ReferenceObject, SchemaObject } from 'openapi3-ts';
import 'reflect-metadata';
import { IOptions } from './options';
export interface ISchemaConverters {
    [validatorType: string]: SchemaConverter | SchemaObject;
}
export type SchemaConverter = (meta: ValidationMetadata, options: IOptions) => ReferenceObject | SchemaObject | void;
export declare const defaultConverters: ISchemaConverters;
