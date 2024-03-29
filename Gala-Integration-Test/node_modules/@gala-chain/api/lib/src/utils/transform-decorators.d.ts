import { ValidationOptions } from "class-validator";
import "reflect-metadata";
type ClassConstructor<Signature = unknown[]> = {
    new (args: Signature): unknown;
};
export declare function ApplyConstructor<ClassInstance, ConstructorSignature, SerializedType>(Constructor: ClassConstructor<ConstructorSignature>, fromTransformer: (propertyValue: SerializedType) => ClassInstance, toTransformer: (classInstance: ClassInstance) => SerializedType): () => (target: Object, propertyKey: string | symbol) => void;
export declare const BigNumberProperty: (opts?: {
    allowInfinity: boolean;
}) => (target: Object, propertyKey: any) => void;
export declare const BigNumberArrayProperty: () => (target: Object, propertyKey: string | symbol) => void;
export declare function EnumProperty(enumType: object, validationOptions?: ValidationOptions): PropertyDecorator;
export declare function StringEnumProperty(enumType: object, validationOptions?: ValidationOptions): PropertyDecorator;
export {};
