import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { reduxForm, InjectedFormProps } from 'redux-form';
import { Button, Card, CardBody, Col, FormGroup } from 'reactstrap';
import { FaChevronRight, FaSpinner, FaRegSave } from 'react-icons/fa';
import { Formik, Field, Form } from 'formik';
import ReduxFormInput from './ReduxFormInput';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as TransactionDataStore from '../store/TransactionData';

// At runtime, Redux will merge together...
type TransactionDataProps =
    TransactionDataStore.TransactionDataState // ... state we've requested from the Redux store
    & typeof TransactionDataStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ startDateIndex: string }>;

interface CustomProps {
    blockNumber: string;
    ethAddress: string;
}

class FetchTransactions extends React.PureComponent<TransactionDataProps & InjectedFormProps<{}, CustomProps>> {
    public componentDidMount() {
        this.ensureDataFetched();
    }

    //public componentDidUpdate() {
    //    this.ensureDataFetched();
    //}

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Transactions</h1>
                {this.renderForm()}
                {this.renderTransactionsTable()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestTransactionData('', '');
    }

    private handleSubmit(values: CustomProps, { setSubmitting }: any) {
        this.props.requestTransactionData(values.blockNumber, values.ethAddress);
        setSubmitting(false);
    }

    private renderForm() {
        return (
            <div>
                <Formik
                    initialValues={{ blockNumber: '9148873', ethAddress: '0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa' }}
                    onSubmit={this.handleSubmit.bind(this)}
                >
                    {({ isSubmitting }) => (
                        <Form>
                            <Col sm="12">
                                <Card className="card-border">
                                    <CardBody>
                                        <FormGroup row={true}>
                                            <Col xs="12" lg="12">
                                                <Field className="col-md-12" type="text" name="blockNumber" />
                                            </Col>
                                        </FormGroup>
                                        <FormGroup row={true}>
                                            <Col xs="12" lg="12">
                                                <Field className="col-md-12" type="text" name="ethAddress" />
                                            </Col>
                                        </FormGroup>
                                    </CardBody>
                                    <div style={{ paddingBottom: 30 }}>
                                        <Button
                                            disabled={isSubmitting}
                                            className="float-right"
                                            color="success"
                                            type="submit"
                                            style={{ marginRight: '10px' }}
                                        >
                                            Next &nbsp;
              <FaChevronRight className="button-padding" size={18} />
                                        </Button>
                                    </div>
                                </Card>
                            </Col>
                        </Form>
                    )}
                </Formik>
                <br />
            </div>
        );
    }

    private renderTransactionsTable() {
        if (!this.props.transactions || this.props.transactions === undefined || this.props.transactions.length == 0) {
            return (<div><h3>No data available</h3></div>);
        }
        else {
            return (
                <table className='table table-striped' aria-labelledby="tabelLabel" style={{ tableLayout: 'fixed', wordWrap: 'break-word' }}>
                    <thead>
                        <tr>
                            <th>Block Hash</th>
                            <th>Block Number</th>
                            <th>Gas</th>
                            <th>Hash</th>
                            <th>From</th>
                            <th>To</th>
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.transactions.map((transaction: TransactionDataStore.TransactionData) =>
                            <tr key={transaction.blockHash}>
                                <td className="col-md-2">{transaction.blockHash}</td>
                                <td className="col-md-2">{transaction.blockNumber}</td>
                                <td className="col-md-2">{transaction.gas}</td>
                                <td className="col-md-2">{transaction.hash}</td>
                                <td className="col-md-2">{transaction.from}</td>
                                <td className="col-md-2">{transaction.to}</td>
                                <td className="col-md-2">{parseInt(transaction.value)}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            );
        }
    }
}

const form = reduxForm<{}, CustomProps>({
    form: 'FetchTransactions',
    fields: ['text'],
    enableReinitialize: true
})(FetchTransactions)

export default connect(
    (state: ApplicationState) => state.transactions, // Selects which state properties are merged into the component's props
    TransactionDataStore.actionCreators // Selects which action creators are merged into the component's props
)(form);