import { BigNumber } from "bignumber.js";
import { ChainObject } from "./ChainObject";
import { AllowanceType } from "./common";
export declare class TokenClaim extends ChainObject {
    static INDEX_KEY: string;
    ownerKey: string;
    collection: string;
    category: string;
    type: string;
    additionalKey: string;
    instance: BigNumber;
    action: AllowanceType;
    issuerKey: string;
    allowanceCreated: number;
    claimSequence: BigNumber;
    created: number;
    quantity: BigNumber;
}
