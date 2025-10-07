import firebase from 'firebase/compat/app';
import 'firebase/compat/auth';
import 'firebase/compat/database';
import 'firebase/compat/firestore';
 

const firebaseConfig = {
  apiKey: "AIzaSyCEFtqau0HbwfgrJuwvRoMZcPCwxX0g2Xg",
  authDomain: "proyecto1-3b095.firebaseapp.com",
  projectId: "proyecto1-3b095",
  storageBucket: "proyecto1-3b095.appspot.com",
  messagingSenderId: "972004404592",
  appId: "1:972004404592:web:9d204923bd5d82876a33b5",
  measurementId: "G-7XHQKB4KPB",
  databaseURL: 'https://reactprojects-b6bf1-default-rtdb.firebaseio.com/',
};

if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
}

const Db = firebase.firestore();
const Auth = firebase.auth();

export { Db, Auth, firebase };
