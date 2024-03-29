import { BigNumber } from "bignumber.js";
import { ChainObject } from "./ChainObject";
import { ChainCallDTO } from "./dtos";
export interface TokenClassKeyProperties {
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
}
export declare class TokenClassKey extends ChainCallDTO {
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    toString(): string;
    toStringKey(): string;
    static toStringKey(props: TokenClassKeyProperties): string;
    allKeysPresent(): boolean;
}
export declare class TokenClass extends ChainObject {
    static INDEX_KEY: string;
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    network: string;
    decimals: number;
    maxSupply: BigNumber;
    isNonFungible: boolean;
    maxCapacity: BigNumber;
    authorities: Array<string>;
    name: string;
    symbol: string;
    description: string;
    contractAddress?: string;
    metadataAddress?: string;
    image: string;
    rarity?: string;
    totalBurned: BigNumber;
    totalMintAllowance: BigNumber;
    knownMintAllowanceSupply?: BigNumber;
    /**
     * Total supply of tokens minted for class.
     *
     * @deprecated 2023-05-30, replaced with knownMintSupply for high-throughput implementation.
     */
    totalSupply: BigNumber;
    knownMintSupply?: BigNumber;
    getKey(): Promise<TokenClassKey>;
    static buildClassKeyList(tokenClassKey: TokenClassKeyProperties): [collection: string, category: string, type: string, additionalKey: string];
    static buildTokenClassCompositeKey(tokenClassKey: TokenClassKeyProperties): string;
    static buildClassKeyObject(token: TokenClassKeyProperties): Promise<TokenClassKey>;
    /**
     * Returns new token class object updated with properties that are allowed to be updated
     */
    updatedWith(toUpdate: ToUpdate): TokenClass;
}
interface ToUpdate {
    name?: string;
    symbol?: string;
    description?: string;
    contractAddress?: string;
    metadataAddress?: string;
    rarity?: string;
    image?: string;
    authorities?: string[];
    overwriteAuthorities?: boolean;
}
export {};
