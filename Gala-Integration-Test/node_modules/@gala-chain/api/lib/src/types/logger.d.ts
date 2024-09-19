export interface ILoggerCommons {
    context: IContextDetails;
    process: IProcessDetails;
    request?: IRequestDetails;
    response?: IResponseDetails;
}
export interface IContextDetails {
    uniqueId: string;
    createdAt: Date;
    channelId: string;
    chaincode: string;
    parameters: string[];
    txId?: string;
    creator: string;
}
export interface IProcessDetails {
    host: string;
    uptime: string;
    loadAvg: number[];
}
export interface IRequestDetails {
    host: string;
    path: string;
    port: string;
    headers: Record<string, unknown>;
}
export interface IResponseDetails {
    isError: boolean;
    statusCode: number;
    message: string;
    payload: Record<string, unknown>;
}
export interface ITimeLogData {
    description: string;
    requestId: string;
    elapsed: string;
    method: string;
    info?: ILoggerCommons;
    metaData?: unknown[];
}
