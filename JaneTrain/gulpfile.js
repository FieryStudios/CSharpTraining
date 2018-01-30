var gulp = require('gulp');

var clean = require('gulp-clean-css');
var concat = require('gulp-concat');
var jshint = require('gulp-jshint');
var rename = require('gulp-rename');
var sass = require('gulp-sass');
var uglify = require('gulp-uglify');


// Compile Our Sass
gulp.task('sass', function() {
    return gulp.src('./styles/sass/*.scss')
        .pipe(sass({outputStyle: 'expanded'}).on('error', sass.logError))
        .pipe(gulp.dest('./styles/css'))
        .pipe(clean())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('./styles/css'));
});

gulp.task('lint', function() {
    return gulp.src('./js/src/*.js')
        .pipe(jshint())
        .pipe(jshint.reporter('default'));
});


// Concatenate & Minify JS
gulp.task('jtScripts', function() {
    return gulp.src('./js/src/*.js')
        .pipe(rename('jt.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('./js/dist'));
});

gulp.task('libScripts', function() {
    return gulp.src('./js/libs/*.js')
        .pipe(concat('libs.js'))
        .pipe(gulp.dest('./js/dist'))
        .pipe(rename('libs.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('./js/dist'));
});

// Watch Files For Changes
gulp.task('watch', function() {
    gulp.watch('./js/src/*.js', ['jtScripts']);
    gulp.watch('./js/libs/*.js', ['libScripts']);
    gulp.watch('./styles/sass/*.scss', ['sass']);
});