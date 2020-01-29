import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as TransactionDataStore from '../store/TransactionData';

// At runtime, Redux will merge together...
type TransactionDataProps =
    TransactionDataStore.TransactionDataState // ... state we've requested from the Redux store
    & typeof TransactionDataStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{}>

class FetchTransactions extends React.PureComponent<TransactionDataProps> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment></<React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestTransactionData();
    }

}

export default connect(
    (state: ApplicationState) => state.transactions, // Selects which state properties are merged into the component's props
    TransactionDataStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchTransactions as any);