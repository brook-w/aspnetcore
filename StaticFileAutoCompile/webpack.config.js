const gulp = require('gulp');
const rimraf = require('rimraf');
const concat = require('gulp-concat');
const uglify = require('gulp-uglify');
const cssmin = require('gulp-cssmin');

const paths = {
    webroot:"./wwwroot/"
}

//paths.js = paths.webroot + "js/**/*.js"
//paths.minJs.js = paths.webroot + "js/**/*.min.js"


//paths.css = paths.webroot + "js/**/*.css"
//paths.minCss.css = paths.webroot + "js/**/*.min.css"

//paths.concatJsDest = paths.webroot + "js/site.min.js";
//paths.concatCssDest = paths.webroot + "css/site.min.css"


gulp.task("clean:js", function(cb) {
    rimraf('',cb)
})


//gulp.task("clean:css", (cb) => {
//    rimraf(paths.concatCssDest,cb)
//})

//gulp.task("clean", ["clean:js", "clean:css"])

//gulp.task("min:js", () => {
    
//})


