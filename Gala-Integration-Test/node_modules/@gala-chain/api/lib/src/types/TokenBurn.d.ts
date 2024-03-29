import { BigNumber } from "bignumber.js";
import { ChainObject } from "./ChainObject";
export declare class TokenBurn extends ChainObject {
    static INDEX_KEY: string;
    burnedBy: string;
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    instance: BigNumber;
    created: number;
    quantity: BigNumber;
}
