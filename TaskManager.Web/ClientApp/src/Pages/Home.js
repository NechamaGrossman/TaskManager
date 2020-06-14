import React from 'react';
import axios from 'axios';
import { produce } from 'immer';
import { HubConnectionBuilder } from '@aspnet/signalr';
import { AccountContext } from '../AccountContext';

class Home extends React.Component {

    state = {
        connection:null,
        tasks: [],
        addedTask: ''
    }
    componentDidMount = async () => {
        const connection = new HubConnectionBuilder().withUrl("/taskhub").build();
        await connection.start();
        await this.refreshTasks();
        connection.on("refresh", tasks => {
            this.setState({tasks:tasks})
        })
        this.setState({ connection });
        
    }
    refreshTasks = async () => {
        const { data } = await axios.get('/api/Task/GetTasks');
        this.setState({ tasks: data });
    }
    onTextChange = e => {
        const nextState = produce(this.state, draftState => {
            draftState.addedTask = e.target.value;
        });
        this.setState(nextState);
    }
    onAddClick = async () => {
        await axios.post('/api/Task/AddTask', { Task: this.state.addedTask });
        this.state.connection.invoke("SendRefresh");
        this.setState({ addedTask: '' })
    }
    onChangeStatusClick = async (TaskId, status) => {
        await axios.post('/api/Task/ChangeTaskStatus', { taskId: TaskId, status: status });
        this.state.connection.invoke("SendRefresh");
    }
    thisCanBeCompletedByUser = (userId, task) => {
        if (task.takenUserId === userId) {
            return false;
        }
        else {
            
            return true;
        }
    }
    generateButtonText = (userId, task) => {
        if (task.takenUserId === userId) {
            return 'Complete the task';
        }
        if (task.takenUserId !== userId) {
            return 'sorry this task is taken';
        }
    }
    render() {
        return (
            <AccountContext.Consumer>
                {value => {
                    const { user } = value;
                    return (
                        <div>
                            <div class="well">
                                <h1>Add a task to the task manager</h1>
                                <input className="form-control" value={this.state.addedTask} name="addedTask" placeholder="Task..." onChange={this.onTextChange} />
                                <br />
                                <br />
                                <button className="btn btn-danger" onClick={this.onAddClick}> Add task</button>
                            </div>
                            <div style={{ backgroundColor: 'white', minHeight: 1000, paddingTop: 10 }}>
                                <table className="table table-hover table-striped table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Task</th>
                                            <th> Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.tasks.map((userTask, key) => {
                                            return (<tr>
                                                <td>{userTask.task}</td>
                                                {userTask.status === 0 && <td> <button onClick={() => { this.onChangeStatusClick(userTask.id, 1) }} className="btn btn-danger">I'm taking this task</button> </td> }
                                                {userTask.status === 3 && <td> <button onClick={() => { this.onChangeStatusClick(userTask.id, 2) }} disabled={this.thisCanBeCompletedByUser(user.id, userTask)} className="btn btn-warning">{this.generateButtonText(user.id, userTask)}.</button> </td>}
                                                {userTask.status == 2 && <td>has been completed</td>}
                                            </tr>)
                                        })}
                                    </tbody>
                                </table>
                            </div>

                        </div>

                    )
                }}
            </AccountContext.Consumer>

        );
    }

}

export default Home;