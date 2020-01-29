import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

export interface TransactionDataState {
    isLoading: boolean;
    transactions: TransactionData[];
}

export interface TransactionData {
    blockHash: string;
    blockNumber: string;
    gas: string;
    hash: string;
    from: string;
    to: string;
    value: string;
}

interface RequestTransactionDataAction {
    type: 'REQUEST_TRANSACTION_DATA';
}

interface ReceiveTransactionDataAction {
    type: 'RECEIVE_TRANSACTION_DATA';
    transactions: TransactionData[];
}

type KnownAction = RequestTransactionDataAction | ReceiveTransactionDataAction;

export const actionCreators = {
    requestTransactionData: (blockNumber: string, ethAddress: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.transactions) {
            fetch(`api/BlockScan`, {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                mode: 'cors', // no-cors, *cors, same-origin
                headers: {
                    'Content-Type': 'application/json'
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                },
                redirect: 'follow', // manual, *follow, error
                referrerPolicy: 'no-referrer', // no-referrer, *client
                body: JSON.stringify({ "blockId": blockNumber, "ethAddress": ethAddress }) // body data type must match "Content-Type" header
            })
                .then(response => {
                    if (response.status && response.status !== 200)
                        return [];
                    else
                        return response.json() as Promise<TransactionData[]>
                })
                .then(data => {
                    debugger;
                    dispatch({ type: 'RECEIVE_TRANSACTION_DATA', transactions: data });
                });

            dispatch({ type: 'REQUEST_TRANSACTION_DATA' });
        }
    }
};

const unloadedState: TransactionDataState = { transactions: [], isLoading: false };

export const reducer: Reducer<TransactionDataState> = (state: TransactionDataState | undefined, incomingAction: Action): TransactionDataState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TRANSACTION_DATA':
            return {
                transactions: state.transactions,
                isLoading: true
            };
        case 'RECEIVE_TRANSACTION_DATA':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            //if (action.startDateIndex === state.startDateIndex) {
            return {
                transactions: action.transactions,
                isLoading: false
            };
            //}
            break;
    }

    return state;
};