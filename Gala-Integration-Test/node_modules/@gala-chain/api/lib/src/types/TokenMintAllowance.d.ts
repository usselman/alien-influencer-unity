import BigNumber from "bignumber.js";
import { ChainObject } from "./ChainObject";
export declare class TokenMintAllowance extends ChainObject {
    static INDEX_KEY: string;
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    totalKnownMintAllowancesAtRequest: BigNumber;
    grantedBy: string;
    grantedTo: string;
    created: number;
    reqId: string;
    quantity: BigNumber;
}
