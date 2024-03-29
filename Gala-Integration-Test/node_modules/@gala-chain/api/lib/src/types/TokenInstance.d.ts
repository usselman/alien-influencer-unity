import { BigNumber } from "bignumber.js";
import { ChainObject } from "./ChainObject";
import { TokenClass, TokenClassKey, TokenClassKeyProperties } from "./TokenClass";
import { ChainCallDTO } from "./dtos";
export interface TokenInstanceKeyProperties {
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    instance: BigNumber;
}
export declare class TokenInstanceKey extends ChainCallDTO {
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    instance: BigNumber;
    static nftKey(c: TokenClassKey | TokenClass | TokenClassKeyProperties, instance: BigNumber | string | number): TokenInstanceKey;
    static fungibleKey(c: TokenClassKey | TokenClass): TokenInstanceKey;
    getTokenClassKey(): TokenClassKey;
    toQueryKey(): TokenInstanceQueryKey;
    toString(): string;
    toStringKey(): string;
    isFungible(): boolean;
}
export declare class TokenInstanceQuantity extends ChainCallDTO {
    tokenInstance: TokenInstanceKey;
    quantity: BigNumber;
    tokenMetadata?: TokenClass;
    getTokenClassKey(this: TokenInstanceQuantity): TokenClassKey;
    toString(this: TokenInstanceQuantity): string;
    toStringKey(this: TokenInstanceQuantity): string;
}
export declare class TokenInstanceQueryKey extends ChainCallDTO {
    collection: string;
    category?: string;
    type?: string;
    additionalKey?: string;
    instance?: BigNumber;
    isCompleteKey(): boolean;
    toCompleteKey(): TokenInstanceKey;
    publicKeyProperties(): string[];
    toQueryParams(): string[];
}
export declare class TokenInstance extends ChainObject {
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    instance: BigNumber;
    isNonFungible: boolean;
    owner?: string;
    static INDEX_KEY: string;
    static FUNGIBLE_TOKEN_INSTANCE: BigNumber;
    GetCompositeKeyString(): string;
    static GetFungibleInstanceFromClass(token: TokenClassKeyProperties): string;
    static CreateCompositeKey(token: TokenInstanceKeyProperties): string;
    static GetCompositeKeyFromString(tokenCid: string): string;
    static buildInstanceKeyList(token: TokenInstanceKeyProperties): [collection: string, category: string, type: string, additionalKey: string, instance: string];
    static buildInstanceKeyObject(token: TokenInstanceKeyProperties): Promise<TokenInstanceKey>;
    static isFungible(instanceId: BigNumber): boolean;
    static isNFT(instanceId: BigNumber): boolean;
}
