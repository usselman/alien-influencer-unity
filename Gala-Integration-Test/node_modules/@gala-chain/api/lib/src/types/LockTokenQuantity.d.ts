import BigNumber from "bignumber.js";
import { TokenInstanceKey } from "./TokenInstance";
export declare class LockTokenQuantity {
    tokenInstanceKey: TokenInstanceKey;
    quantity: BigNumber;
    owner?: string;
}
