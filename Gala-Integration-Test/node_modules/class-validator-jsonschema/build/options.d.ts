import type { MetadataStorage as ClassTransformerMetadataStorage } from 'class-transformer/types/MetadataStorage';
import { MetadataStorage, ValidatorOptions } from 'class-validator';
import { ISchemaConverters } from './defaultConverters';
export interface IOptions extends ValidatorOptions {
    additionalConverters: ISchemaConverters;
    classTransformerMetadataStorage?: ClassTransformerMetadataStorage;
    classValidatorMetadataStorage: MetadataStorage;
    refPointerPrefix: string;
    schemaNameField: string;
}
export declare const defaultOptions: IOptions;
