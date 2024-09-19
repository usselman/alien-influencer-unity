import { BigNumber } from "bignumber.js";
import { ValidationFailedError } from "../utils";
import { ChainObject } from "./ChainObject";
import { TokenClassKeyProperties } from "./TokenClass";
export declare class TokenNotInBalanceError extends ValidationFailedError {
    constructor(owner: string, tokenClass: TokenClassKeyProperties, instanceId: BigNumber);
}
export declare class TokenLockedError extends ValidationFailedError {
    constructor(owner: string, tokenClass: TokenClassKeyProperties, instanceId: BigNumber, name: string | undefined);
}
export declare class TokenNotLockedError extends ValidationFailedError {
    constructor(owner: string, tokenClass: TokenClassKeyProperties, instanceId: BigNumber);
}
export declare class TokenQuantityNotUnlockedError extends ValidationFailedError {
    constructor(owner: string, tokenClass: TokenClassKeyProperties, quantity: BigNumber, name: string | undefined);
}
export declare class TokenInUseError extends ValidationFailedError {
    constructor(owner: string, tokenClass: TokenClassKeyProperties, instanceId: BigNumber);
}
export declare class TokenNotInUseError extends ValidationFailedError {
    constructor(owner: string, tokenClass: TokenClassKeyProperties, instanceId: BigNumber);
}
export declare class TokenBalance extends ChainObject {
    static readonly INDEX_KEY = "GCTB";
    readonly owner: string;
    readonly collection: string;
    readonly category: string;
    readonly type: string;
    readonly additionalKey: string;
    constructor(params?: {
        owner: string;
        collection: string;
        category: string;
        type: string;
        additionalKey: string;
    });
    /**
     * Token instance IDs for NFTs. It is also used to determine if the balance is
     * for fungible or non-fungible tokens. If the array is undefined, then the
     * balance is for fungible tokens.
     */
    private instanceIds?;
    private lockedHolds?;
    private inUseHolds?;
    private quantity;
    getNftInstanceCount(): number;
    getUnexpiredLockedHolds(currentTime: number): TokenHold[];
    getUnexpiredLockedHoldsSortedByAscendingExpiration(currentTime: number): TokenHold[];
    getUnexpiredInUseHolds(currentTime: number): TokenHold[];
    ensureCanAddInstance(instanceId: BigNumber): {
        add(): void;
    };
    ensureCanRemoveInstance(instanceId: BigNumber, currentTime: number): {
        remove(): void;
    };
    ensureCanLockInstance(hold: TokenHold, currentTime: number): {
        lock(): void;
    };
    ensureCanUnlockInstance(instanceId: BigNumber, name: string | undefined, currentTime: number): {
        unlock(): void;
    };
    ensureCanUseInstance(hold: TokenHold, currentTime: number): {
        use(): void;
    };
    ensureCanReleaseInstance(instanceId: BigNumber, name: string | undefined, currentTime: number): {
        release(): void;
    };
    clearHolds(instanceId: BigNumber, currentTime: number): void;
    findLockedHold(instanceId: BigNumber, name: string | undefined, currentTime: number): TokenHold | undefined;
    findInUseHold(instanceId: BigNumber, currentTime: number): TokenHold | undefined;
    containsAnyNftInstanceId(): boolean;
    isInstanceSpendable(instanceId: BigNumber, currentTime: number): boolean;
    getNftInstanceIds(): BigNumber[];
    cleanupExpiredHolds(currentTime: number): TokenBalance;
    private containsInstance;
    private isInstanceLocked;
    private isInstanceInUse;
    private ensureInstanceIsNft;
    private ensureInstanceIsInBalance;
    private ensureInstanceIsNotLockedWithTheSameName;
    private ensureInstanceIsNotLocked;
    private ensureInstanceIsNotUsed;
    getQuantityTotal(): BigNumber;
    getSpendableQuantityTotal(currentTime: number): BigNumber;
    getLockedQuantityTotal(currentTime: number): BigNumber;
    ensureCanAddQuantity(quantity: BigNumber): {
        add(): void;
    };
    ensureCanSubtractQuantity(quantity: BigNumber, currentTime: number): {
        subtract(): void;
    };
    private ensureQuantityIsSpendable;
    private ensureTokenQuantityHoldIsFungible;
    ensureCanLockQuantity(hold: TokenHold): {
        lock(): void;
    };
    private isMatchingHold;
    ensureCanUnlockQuantity(quantity: BigNumber, currentTime: number, name?: string, lockAuthority?: string): {
        unlock(): void;
    };
    private getCurrentLockedQuantity;
    private ensureContainsNoNftInstances;
    private ensureIsValidQuantityForFungible;
}
export declare class TokenHold {
    static readonly DEFAULT_EXPIRES = 0;
    readonly createdBy: string;
    readonly instanceId: BigNumber;
    readonly quantity: BigNumber;
    readonly created: number;
    readonly expires: number;
    readonly name?: string;
    lockAuthority?: string;
    constructor(params?: {
        createdBy: string;
        instanceId: BigNumber;
        quantity: BigNumber;
        created: number;
        expires?: number;
        name?: string;
        lockAuthority?: string;
    });
    static createValid(params: {
        createdBy: string;
        instanceId: BigNumber;
        quantity: BigNumber;
        created: number;
        expires: number | undefined;
        name: string | undefined;
        lockAuthority: string | undefined;
    }): Promise<TokenHold>;
    matches(instanceId: BigNumber, name: string | undefined): boolean;
    isExpired(currentTime: number): boolean;
    static sortByAscendingExpiration(a: TokenHold, b: TokenHold): 0 | 1 | -1;
}
