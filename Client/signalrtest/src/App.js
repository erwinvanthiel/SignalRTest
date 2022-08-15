import logo from './logo.svg';
import './App.css';
import React from "react";
import { useRef } from "react";
import * as signalR from "@microsoft/signalr";



function App() {


  // reference to input fields
  let toInput = React.createRef();  
  let messageInput = React.createRef();  
  let nameInput = React.createRef();
  let onlineParagraph = React.createRef();
  let passwordInput = React.createRef();
  let passwordLogInInput = React.createRef();
  let nameLogInInput = React.createRef();


  // Websocker signalR stuff
  const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://192.168.56.1:8885/MyHub')
    .withAutomaticReconnect()
    .build();

  // send message to another client by name
  function sendMessage() {
      let to = toInput.current.value;
      let message = messageInput.current.value;
      connection.send("SendMessage", to, message);
  }

  // register myself as a client by a certain name
  function register() {
      let name = nameInput.current.value;
      let pwd = passwordInput.current.value;
      connection.send("register", name, pwd);
  }

  // register myself as a client by a certain name
  function logIn() {
      let name = nameLogInInput.current.value;
      let pwd = passwordLogInInput.current.value;
      connection.send("LogIn", name, pwd);
  }

  // This is how respond when I receive a message from the server or from another client via the server
  connection.on("receiveMessage", (sender, message) => {
      let string = sender + " says: " + message;
      alert(string);
  });

  // when the server notifies me I need to update my list of clients online.
  connection.on("update", () => {
    let onlineUsers = httpGet("http://192.168.56.1:8885/clients");
    onlineParagraph.current.value = onlineUsers;
  });

  // Ask the server whos online.
  function httpGet(theUrl)
  {
      var xmlhr = new XMLHttpRequest();
      xmlhr.open( "GET", theUrl, false);
      xmlhr.send( null );
      return xmlhr.responseText;
  }

  function connectFailed(){
    console.log("Connection failed");
  }

  function connectSucceeded(){
    console.log("Connection succeeded");
  }

  connection.start().then(connectSucceeded, connectFailed);

  // render some html stuff
  return (
    <div className="App">
        <h1>Register</h1>
        <input ref={nameInput}/>
        <input ref={passwordInput}/><button onClick={register}>Send</button>

        <h1>Log in</h1>
        <input ref={nameLogInInput}/>
        <input ref={passwordLogInInput}/><button onClick={logIn}>Send</button>

        <h1>Send message</h1>
        <input ref={toInput}/><br />
        <input ref={messageInput}/><br />
        <button onClick={sendMessage}>Send</button>
        <h1>Currently online:</h1>
        <input ref={onlineParagraph} />
    </div>
  );
}

export default App;
