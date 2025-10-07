import firebase from 'firebase/compat/app';
import 'firebase/compat/auth';
import 'firebase/compat/database';

const firebaseConfig = {
  apiKey: '',
  authDomain: '',
  projectId: '',
  storageBucket: '',
  messagingSenderId: '',
  appId: '',
  measurementId: '',
  databaseURL: '',
};

if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
}

const Db = firebase.database();
const Auth = firebase.auth();

export { Db, Auth, firebase };
