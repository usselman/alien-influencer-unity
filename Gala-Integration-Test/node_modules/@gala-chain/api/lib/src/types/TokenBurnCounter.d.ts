import { BigNumber } from "bignumber.js";
import { RangedChainObject } from "./RangedChainObject";
export declare class TokenBurnCounter extends RangedChainObject {
    static INDEX_KEY: string;
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    timeKey: string;
    burnedBy: string;
    instance: BigNumber;
    totalKnownBurnsCount: BigNumber;
    created: number;
    quantity: BigNumber;
    referenceId: string;
    epoch: string;
    referencedBurnId(): string;
}
