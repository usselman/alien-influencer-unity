import "reflect-metadata";
export interface ChainKeyConfig {
    position: number;
}
export interface ChainKeyMetadata extends ChainKeyConfig {
    key: string | symbol;
}
export declare function ChainKey(chainKeyConfig: ChainKeyConfig): PropertyDecorator;
