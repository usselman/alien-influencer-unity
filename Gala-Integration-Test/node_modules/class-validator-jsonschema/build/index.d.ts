import { ValidationMetadata } from 'class-validator/types/metadata/ValidationMetadata';
import type { SchemaObject } from 'openapi3-ts';
import { IOptions } from './options';
export { JSONSchema } from './decorators';
export declare function validationMetadatasToSchemas(userOptions?: Partial<IOptions>): Record<string, SchemaObject>;
export declare function validationMetadataArrayToSchemas(metadatas: ValidationMetadata[], userOptions?: Partial<IOptions>): Record<string, SchemaObject>;
export declare function targetConstructorToSchema(targetConstructor: Function, userOptions?: Partial<IOptions>): SchemaObject;
