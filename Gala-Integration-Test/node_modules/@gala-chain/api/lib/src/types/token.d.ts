import BigNumber from "bignumber.js";
import { TokenBalance } from "./TokenBalance";
import { TokenClass, TokenClassKey } from "./TokenClass";
import { TokenInstanceKey } from "./TokenInstance";
import { ChainCallDTO } from "./dtos";
export declare class FetchTokenClassesDto extends ChainCallDTO {
    tokenClasses: Array<TokenClassKey>;
}
export declare class FetchTokenClassesWithPaginationDto extends ChainCallDTO {
    static readonly MAX_LIMIT: number;
    static readonly DEFAULT_LIMIT = 1000;
    collection?: string;
    category?: string;
    type?: string;
    additionalKey?: string;
    bookmark?: string;
    limit?: number;
}
export declare class FetchTokenClassesResponse extends ChainCallDTO {
    results: TokenClass[];
    nextPageBookmark?: string;
}
export declare class FetchTokenInstancesDto extends ChainCallDTO {
    tokenInstances: Array<TokenInstanceKey>;
}
export declare class CreateTokenClassDto extends ChainCallDTO {
    static DEFAULT_NETWORK: string;
    static DEFAULT_DECIMALS: number;
    static DEFAULT_MAX_CAPACITY: BigNumber;
    static DEFAULT_MAX_SUPPLY: BigNumber;
    static INITIAL_MINT_ALLOWANCE: BigNumber;
    static INITIAL_TOTAL_SUPPLY: BigNumber;
    static INITIAL_TOTAL_BURNED: BigNumber;
    network?: string;
    decimals?: number;
    maxCapacity?: BigNumber;
    maxSupply?: BigNumber;
    tokenClass: TokenClassKey;
    name: string;
    symbol: string;
    description: string;
    totalMintAllowance?: BigNumber;
    totalSupply?: BigNumber;
    totalBurned?: BigNumber;
    contractAddress?: string;
    metadataAddress?: string;
    rarity?: string;
    image: string;
    isNonFungible?: boolean;
    authorities?: string[];
}
export declare class UpdateTokenClassDto extends ChainCallDTO {
    tokenClass: TokenClassKey;
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
export declare class FetchBalancesDto extends ChainCallDTO {
    owner?: string;
    collection?: string;
    category?: string;
    type?: string;
    additionalKey?: string;
}
export declare class FetchBalancesWithPaginationDto extends ChainCallDTO {
    static readonly MAX_LIMIT: number;
    static readonly DEFAULT_LIMIT = 1000;
    owner?: string;
    collection?: string;
    category?: string;
    type?: string;
    additionalKey?: string;
    bookmark?: string;
    limit?: number;
}
export declare class TokenBalanceWithMetadata extends ChainCallDTO {
    balance: TokenBalance;
    token: TokenClass;
}
export declare class FetchBalancesWithTokenMetadataResponse extends ChainCallDTO {
    results: TokenBalanceWithMetadata[];
    nextPageBookmark?: string;
}
export declare class TransferTokenDto extends ChainCallDTO {
    from?: string;
    to: string;
    tokenInstance: TokenInstanceKey;
    quantity: BigNumber;
    useAllowances?: Array<string>;
}
