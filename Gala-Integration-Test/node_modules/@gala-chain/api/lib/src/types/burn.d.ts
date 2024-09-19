import BigNumber from "bignumber.js";
import { BurnTokenQuantity } from "./BurnTokenQuantity";
import { TokenBurnCounter } from "./TokenBurnCounter";
import { ChainCallDTO } from "./dtos";
import { BatchMintTokenDto } from "./mint";
export declare class FetchBurnsDto extends ChainCallDTO {
    burnedBy: string;
    collection?: string;
    category?: string;
    type?: string;
    additionalKey?: string;
    instance?: string;
    created?: number;
}
export declare class BurnTokensDto extends ChainCallDTO {
    tokenInstances: Array<BurnTokenQuantity>;
    owner?: string;
}
export declare class BurnAndMintDto extends ChainCallDTO {
    static MAX_ARR_SIZE: number;
    burnDto: BurnTokensDto;
    burnOwner: string;
    mintDto: BatchMintTokenDto;
}
export declare class FetchBurnCountersWithPaginationDto extends ChainCallDTO {
    static readonly MAX_LIMIT: number;
    static readonly DEFAULT_LIMIT = 1000;
    collection?: string;
    category?: string;
    type?: string;
    additionalKey?: string;
    bookmark?: string;
    limit?: number;
}
export declare class FetchBurnCountersResponse extends ChainCallDTO {
    results: TokenBurnCounter[];
    nextPageBookmark?: string;
}
export declare class TokenBurnCounterCompositeKeyDto extends ChainCallDTO {
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    timeKey: string;
    burnedBy: string;
    instance: BigNumber;
    totalKnownBurnsCount: BigNumber;
}
