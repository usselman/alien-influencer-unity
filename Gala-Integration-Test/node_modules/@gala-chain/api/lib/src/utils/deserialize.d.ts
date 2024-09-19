type Base<T, BaseT> = T extends BaseT ? T : never;
export type Inferred<T, BaseT = any> = T extends (infer U)[] ? Base<U, BaseT> : Base<T, BaseT>;
export interface ClassConstructor<T> {
    new (...args: unknown[]): T;
}
export default function customDeserialize<T, BaseT = unknown>(constructor: ClassConstructor<Inferred<T, BaseT>>, object: string | Record<string, unknown> | Record<string, unknown>[]): T;
export {};
