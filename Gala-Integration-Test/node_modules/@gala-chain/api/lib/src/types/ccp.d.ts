export interface ICCP {
    name: string;
    version: string;
    client: {
        organization: string;
        connection: {
            timeout: {
                peer: {
                    endorser: string;
                };
            };
        };
    };
    organizations: {
        [orgName: string]: {
            mspid: string;
            peers: string[];
            certificateAuthorities: string[];
        };
    };
    peers: {
        [hostname: string]: {
            url: string;
            tlsCACerts: {
                pem: string;
            };
            grpcOptions: {
                string: string;
                hostnameOverride: string;
            };
        };
    };
    certificateAuthorities: {
        [hostname: string]: {
            url: string;
            caName: string;
            tlsCACerts: {
                pem: string[];
            };
            httpOptions: {
                verify: false;
            };
        };
    };
}
